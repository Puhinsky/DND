using Puhinsky.DND.Models;
using SimpleFileBrowser;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class MapSetupController : VisualElement
    {
        private readonly Button _importMap = new();
        private readonly IntegerField _mapScale = new(_scaleLabel, 1000);
        private readonly VisualElement _previewMap = new();

        private const string _importLabel = "Импортировать карту ...";
        private const string _scaleLabel = "Пикселей на метр";

        private const string _mapSetupClass = "map-setup";
        private const string _buttonClass = "map-button";
        private const string _previewClass = "map-preview";

        private readonly MapView _mapView;

        public MapModel Model { get; private set; }

        public MapSetupController()
        {
            Model = new MapModel();
            Model.Texture.Changed += (Texture2D texture) =>
            {
                _previewMap.style.backgroundImage = texture;
            };

            _mapView = Object.FindAnyObjectByType<MapView>();
            _mapView.SetModel(Model);

            style.flexGrow = 1;
            _previewMap.style.flexGrow = 1;
            _importMap.text = _importLabel;
            _importMap.clicked += OnMapImportBegin;
            Model.PixelsPerUnit.BindView(_mapScale, nameof(_mapScale.value), BindingMode.TwoWay);
            Add(_importMap);
            Add(_mapScale);
            Add(_previewMap);
            AddToClassList(_mapSetupClass);
            _importMap.AddToClassList(_buttonClass);
            _previewMap.AddToClassList(_previewClass);
        }

        private void OnMapImportBegin()
        {
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Image files", new string[] { "png", "jpg", "jpeg" }));
            FileBrowser.ShowLoadDialog(OnMapImported, null, FileBrowser.PickMode.Files, allowMultiSelection: false, title: "Загрузить карту");
        }

        private void OnMapImported(string[] paths)
        {
            if (paths.Length <= 0)
                return;

            var filePath = paths.First();

            if (File.Exists(filePath))
            {
                Model.Texture.SetWithoutNotify(new Texture2D(1, 1));
                Model.Texture.Value.LoadImage(File.ReadAllBytes(filePath));
                Model.Texture.Notify();
            }
        }
    }
}
