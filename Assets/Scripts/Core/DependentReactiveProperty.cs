using System;

namespace Puhinsky.DND.Core
{
    [Serializable]
    public class DependentReactiveProperty<T> : ReactiveProperty<T>
    {
        public delegate T ReactiveExpression();

        private readonly ReactiveExpression _expression;

        public DependentReactiveProperty(ReactiveExpression expression)
        {
            _expression = expression;
        }

        public override T Value { get => base.Value; }

        public void AddDependency<D>(params ReactiveProperty<D>[] dependencies)
        {
            foreach (var dependency in dependencies)
            {
                dependency.TypelessChanged += OnUpdate;
            }

            OnUpdate();
        }

        public void RemoveDependency<D>(params ReactiveProperty<D>[] dependencies)
        {
            foreach (var dependency in dependencies)
            {
                dependency.TypelessChanged -= OnUpdate;
            }

            OnUpdate();
        }

        private void OnUpdate()
        {
            Value = _expression();
        }
    }
}
