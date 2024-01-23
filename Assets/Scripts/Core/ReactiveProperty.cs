using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.Core
{
    public class ReactiveProperty<T>
    {
        public event Action<T> Changed;

        private T _value;

        [SerializeField]
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }

        public ReactiveProperty() { }
        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public void BindView(VisualElement view, BindingId binding, BindingMode mode)
        {
            view.dataSource = this;
            view.SetBinding(binding, new DataBinding()
            {
                dataSourcePath = PropertyPath.FromName(nameof(Value)),
                bindingMode = mode
            });
        }

        public void SetWithoutNotify(T value)
        {
            _value = value;
        }

        public void Notify()
        {
            Changed?.Invoke(_value);
        }
    }
}
