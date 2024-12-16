#region "Usings"

using Windows.UI;

#endregion

namespace AdventOfCode.WinUI.Utils;

/// <summary>
///     Represents a color in the RGB or HSL color space.
/// </summary>
public readonly struct AdventColor
{
    /// <summary>
    ///     The red component of the color.
    /// </summary>
    public byte Red { get; }

    /// <summary>
    ///     The green component of the color.
    /// </summary>
    public byte Green { get; }

    /// <summary>
    ///     The blue component of the color.
    /// </summary>
    public byte Blue { get; }

    /// <summary>
    ///     The hue component of the color.
    /// </summary>
    public double Hue { get; }

    /// <summary>
    ///     The saturation component of the color.
    /// </summary>
    public double Saturation { get; }

    /// <summary>
    ///     The lightness component of the color.
    /// </summary>
    public double Lightness { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AdventColor" /> struct.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    public AdventColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Hue = 0;
        Saturation = 0;
        Lightness = 0;
    }

    /// <summary>
    ///     Determines whether the color has RGB values.
    /// </summary>
    public bool HasRgb => Red != 0 || Green != 0 || Blue != 0;

    /// <summary>
    ///     Determines whether the color has HSL values.
    /// </summary>
    public bool HasHsl => Hue != 0 || Saturation != 0 || Lightness != 0;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AdventColor" /> struct.
    /// </summary>
    /// <param name="hue">The hue component of the color.</param>
    /// <param name="saturation">The saturation component of the color.</param>
    /// <param name="lightness">The lightness component of the color.</param>
    public AdventColor(double hue, double saturation, double lightness)
    {
        Red = 0;
        Green = 0;
        Blue = 0;
        Hue = hue;
        Saturation = saturation;
        Lightness = lightness;
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="AdventColor" /> struct with the specified RGB values.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    /// <returns></returns>
    public static AdventColor FromRgb(byte red, byte green, byte blue) => new(red, green, blue);

    /// <summary>
    ///     Creates a new instance of the <see cref="AdventColor" /> struct with the specified HSL values.
    /// </summary>
    /// <param name="hue">The hue component of the color.</param>
    /// <param name="saturation">The saturation component of the color.</param>
    /// <param name="lightness">The lightness component of the color.</param>
    /// <returns></returns>
    public static AdventColor FromHsl(double hue, double saturation, double lightness) =>
        new(hue, saturation, lightness);

    /// <summary>
    ///     Generates a color based on the hue, saturation and lightness values.
    /// </summary>
    /// <returns></returns>
    public AdventColor GenerateColor()
    {
        // calculate chroma (the color intensity)
        var chroma = (1 - Math.Abs(2 * Lightness - 1)) * Saturation;

        // helper value to ensure the rgb component transition smoothly as the hue changes
        var x = chroma * (1 - Math.Abs(Hue / 60 % 2 - 1));

        // offset for the rgb values to adjust the lightness
        var m = Lightness - chroma / 2;

        // calculate the rgb values based on the hue
        var (r, g, b) = Hue switch
        {
            >= 0 and < 60 => (chroma, x, 0d),
            >= 60 and < 120 => (x, chroma, 0d),
            var _ => (0d, 0d, 0d),
        };

        // add the offset to the rgb values and convert them to byte values
        return FromRgb(ToColorValue(r, m), ToColorValue(g, m), ToColorValue(b, m));
    }

    /// <summary>
    ///     Converts <see cref="AdventColor" /> to <see cref="Color" />.
    /// </summary>
    /// <returns></returns>
    public Color ToColor() => Color.FromArgb(0xff, Red, Green, Blue);

    /// <summary>
    ///     Converts the color value to a byte value.
    /// </summary>
    /// <param name="colVal"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    private static byte ToColorValue(double colVal, double m)
    {
        // adjust value by adding offset to color value to account for lightness
        // scale the final value to the byte range
        var val = (colVal + m) * 255;

        return (byte)Math.Round(val);
    }
}
