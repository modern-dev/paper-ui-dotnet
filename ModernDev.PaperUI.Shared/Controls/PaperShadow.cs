using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace ModernDev.PaperUI
{
    public class PaperShadow : ContentControl, IPaperShadowElement
    {
        #region Constructor

        static PaperShadow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PaperShadow),
                new FrameworkPropertyMetadata(typeof (PaperShadow)));
        }

        #endregion

        #region Fields

        protected Border ShadowBorder;

        #endregion

        #region Properties

        public static readonly DependencyProperty ElevationProperty = DependencyProperty.Register("Elevation",
            typeof (ShadowElevations), typeof (PaperShadow),
            new FrameworkPropertyMetadata(ShadowElevations.None, ElevationChange));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius",
            typeof(double), typeof(PaperShadow), new FrameworkPropertyMetadata(0.0, CornerRadiusChange));

        public ShadowElevations Elevation
        {
            get { return (ShadowElevations)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        public double CornerRadius
        {
            get { return (double) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static List<ShadowElevationData> ElevationData = new List<ShadowElevationData>
        {
            new ShadowElevationData(0.0, 0, 0),
            new ShadowElevationData(0.16, 2, 5),
            new ShadowElevationData(0.2, 8, 17),
            new ShadowElevationData(0.24, 12, 15),
            new ShadowElevationData(0.22, 16, 28),
            new ShadowElevationData(0.2, 27, 24)
        };

        #endregion

        #region Methods

        private static void ElevationChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var shadowData = ElevationData[(int) (ShadowElevations) e.NewValue];
            var shadow = (DropShadowEffect) ((PaperShadow) sender)?.ShadowBorder?.Effect;
            var animDuration = TimeSpan.FromSeconds(0.3);

            shadow?.BeginAnimation(DropShadowEffect.OpacityProperty, new DoubleAnimation
            {
                Duration = animDuration,
                To = shadowData.Opacity
            });
            shadow?.BeginAnimation(DropShadowEffect.BlurRadiusProperty, new DoubleAnimation
            {
                Duration = animDuration,
                To = shadowData.BlurRadius
            });
            shadow?.BeginAnimation(DropShadowEffect.ShadowDepthProperty, new DoubleAnimation
            {
                Duration = animDuration,
                To = shadowData.ShadowDepth
            });
        }

        private static void CornerRadiusChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var border = ((PaperShadow) sender)?.ShadowBorder;

            var anim = new ObjectAnimationUsingKeyFrames();
            anim.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius((double) e.OldValue),
                TimeSpan.FromSeconds(0.0)));
            anim.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius((double) e.NewValue),
                TimeSpan.FromSeconds(0.3)));

            border?.BeginAnimation(Border.CornerRadiusProperty, anim);
        }

        public override void OnApplyTemplate()
        {
            ShadowBorder = (Border) GetTemplateChild("ShadowBorder");

            base.OnApplyTemplate();
        }

        #endregion
    }
}