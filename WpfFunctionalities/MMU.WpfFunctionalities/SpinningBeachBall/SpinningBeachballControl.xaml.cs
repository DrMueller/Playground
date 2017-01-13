using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MMU.WpfFunctionalities.Animations
{
    /// <summary>
    /// Interaction logic for SpinningBeachball.xaml
    /// </summary>
    public partial class SpinningBeachball : UserControl
    {
        public static DependencyProperty IsSpinningProperty =
            DependencyProperty.Register("IsSpinning",
                typeof(bool),
                typeof(SpinningBeachball),
                new PropertyMetadata(IsSpinningChangedCallback));
        public static DependencyProperty SpinningDurationProperty =
            DependencyProperty.Register("SpinningDuration",
                typeof(Duration),
                typeof(SpinningBeachball),
                new PropertyMetadata(new Duration(new TimeSpan(0, 0, 7)), SpinningDurationChangedCallback));
        private static Storyboard _storyboardSpinning;

        public SpinningBeachball()
        {
            InitializeComponent();
        }

        #region Properties

        public bool IsSpinning
        {
            get
            {
                return (bool)GetValue(IsSpinningProperty);
            }
            set
            {
                SetAnimation(this, value);
                SetValue(IsSpinningProperty, value);
            }
        }

        public Duration SpinningDuration
        {
            get
            {
                return (Duration)GetValue(SpinningDurationProperty);
            }
            set
            {
                SetValue(SpinningDurationProperty, value);
                CreateStoryboard(this, value);
            }
        }

        #endregion

        #region Private Methods

        private static DoubleAnimation CreateSpinningAnimatition(Duration duration)
        {
            var result = new DoubleAnimation(0, 360, duration, FillBehavior.Stop);
            result.RepeatBehavior = RepeatBehavior.Forever;
            result.By = 1;
            result.BeginAnimation(RotateTransform.AngleProperty, result);

            return result;
        }

        private static void CreateStoryboard(SpinningBeachball spinningBeachball, Duration duration)
        {
            var spinningAnimation = CreateSpinningAnimatition(duration);
            var anglePath = new PropertyPath("RenderTransform.Angle");
            var storyBoard = new Storyboard();
            storyBoard.RepeatBehavior = RepeatBehavior.Forever;
            storyBoard.Children.Add(spinningAnimation);
            Storyboard.SetTargetProperty(spinningAnimation, anglePath);
            Storyboard.SetTarget(spinningAnimation, spinningBeachball.ImgBeachball);
            _storyboardSpinning = storyBoard;
        }

        private static void IsSpinningChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spinningBeachball = (SpinningBeachball)d;
            var isSpinning = (bool)e.NewValue;
            SetAnimation(spinningBeachball, isSpinning);
        }

        private static void SetAnimation(SpinningBeachball spinningBeachball, bool running)
        {
            if (_storyboardSpinning == null)
            {
                var duration = (Duration)spinningBeachball.SpinningDuration;
                CreateStoryboard(spinningBeachball, duration);
            }

            if (running)
            {
                _storyboardSpinning.Begin();
            }
            else
            {
                _storyboardSpinning.Stop();
            }
        }

        private static void SpinningDurationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spinningBeachBall = (SpinningBeachball)d;
            var duration = (Duration)e.NewValue;
            CreateStoryboard(spinningBeachBall, duration);
        }

        #endregion
    }
}