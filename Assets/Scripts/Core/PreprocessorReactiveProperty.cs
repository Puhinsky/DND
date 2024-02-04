namespace Puhinsky.DND.Core
{
    public class PreprocessorReactiveProperty<T> : ReactiveProperty<T>
    {
        public delegate T Preprocessor(T value);

        private Preprocessor _preprocessor;

        public PreprocessorReactiveProperty(Preprocessor preprocessor, T value) : base(value)
        {
            _preprocessor = preprocessor;
        }

        public PreprocessorReactiveProperty(Preprocessor preprocessor)
        {
            _preprocessor = preprocessor;
        }

        public PreprocessorReactiveProperty(T value) : base(value) { }
        public PreprocessorReactiveProperty() { }

        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = _preprocessor(value);
            }
        }

        public void SetPreprocessor(Preprocessor preprocessor)
        {
            _preprocessor = preprocessor;
        }
    }
}

