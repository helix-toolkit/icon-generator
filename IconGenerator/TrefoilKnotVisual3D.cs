namespace IconGenerator
{
    using System;
    using System.Windows;
    using System.Windows.Media.Media3D;

    using HelixToolkit.Wpf;

    /// <summary>
    /// Represents a 3D visual showing a trefoil knot.
    /// </summary>
    /// <remarks>See <a href="https://en.wikipedia.org/wiki/Trefoil_knot">Wikipedia</a>.</remarks>
    public class TrefoilKnotVisual3D : ParametricSurface3D
    {
        /// <summary>
        /// Identifies the <see cref="B"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BProperty =
            DependencyProperty.Register("B", typeof(double), typeof(TrefoilKnotVisual3D), new PropertyMetadata(3d, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="C1"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty C1Property =
            DependencyProperty.Register("C1", typeof(double), typeof(TrefoilKnotVisual3D), new PropertyMetadata(10d, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="C2"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty C2Property =
            DependencyProperty.Register("C2", typeof(double), typeof(TrefoilKnotVisual3D), new PropertyMetadata(2d, GeometryChanged));

        /// <summary>
        /// Gets or sets the b parameter. The default is 3.
        /// </summary>
        /// <value>The b value.</value>
        public double B
        {
            get { return (double)this.GetValue(BProperty); }
            set { this.SetValue(BProperty, value); }
        }

        /// <summary>
        /// Gets or sets the c1 parameter. The default is 10.
        /// </summary>
        /// <value>The c1 value.</value>
        public double C1
        {
            get { return (double)this.GetValue(C1Property); }
            set { this.SetValue(C1Property, value); }
        }

        /// <summary>
        /// Gets or sets the c2 parameter. The default is 2.
        /// </summary>
        /// <value>The c2 value.</value>
        public double C2
        {
            get { return (double)this.GetValue(C2Property); }
            set { this.SetValue(C2Property, value); }
        }

        /// <summary>
        /// Evaluates the surface at the specified u,v parameters.
        /// </summary>
        /// <param name="u">The u parameter.</param>
        /// <param name="v">The v parameter.</param>
        /// <param name="uv">The texture coordinates.</param>
        /// <returns>The evaluated <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
        protected override Point3D Evaluate(double u, double v, out Point uv)
        {
            u *= 4 * Math.PI;
            v = (v - 0.5) * 2 * Math.PI;

            double b = this.B;

            double cosu = Math.Cos(u);
            double sinu = Math.Sin(u);
            double cosv = Math.Cos(v);
            double sinv = Math.Sin(v);

            double c1 = this.C1;
            double c2 = this.C2;
            double c3 = c1 * (Math.Cos(b * u / 2) + b) / 4;
            double c4 = -c3 * sinu - b * c1 * Math.Sin(b * u / 2) * cosu / 8;
            double c5 = c3 * cosu - b * c1 * Math.Sin(b * u / 2) * sinu / 8;
            double c6 = (b * c3 * Math.Cos(Math.Sin(b * u / 2)) * Math.Cos(b * u / 2)) / 2 - (b * c1 * Math.Sin(Math.Sin(b * u / 2)) * Math.Sin(b * u / 2)) / 8;
            double c7 = Math.Sqrt(c4 * c4 + c5 * c5);
            double c8 = Math.Sqrt(c4 * c4 + c5 * c5 + c6 * c6);

            var x = c3 * cosu + (c2 * (c8 * cosv * c5 - sinv * c4 * c6) / (c7 * c8));
            var y = c3 * sinu - (c2 * (c8 * cosv * c4 + sinv * c5 * c6) / (c7 * c8));
            var z = c3 * Math.Sin(Math.Sin(b * u / 2)) + (c2 * sinv * c7 / c8);

            uv = new Point(u, v);
            return new Point3D(x, y, z);
        }
    }
}