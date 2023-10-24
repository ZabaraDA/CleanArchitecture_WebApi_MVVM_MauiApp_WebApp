using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Behaviours
{
    public class FadingLabelAnimationBehavior : Behavior<Label>
    {
        private Label _associatedObject;

        public uint DesiredDuration { get; set; } = 750;

        protected override void OnAttachedTo(Label bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;

            _associatedObject.PropertyChanged += OnTextChangedHandler;
        }

        private async void OnTextChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                _associatedObject.Opacity = 0f;
                await _associatedObject.FadeTo(1f, DesiredDuration);
            }
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            _associatedObject = null;
            base.OnDetachingFrom(bindable);

            _associatedObject.PropertyChanged -= OnTextChangedHandler;
        }
    }
}
