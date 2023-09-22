using System.Text;

namespace CC.CSX;

/// <summary>
/// A node that does not render itself, but just its children.
/// </summary>
public class Fragment : HtmlNode
{
    public Fragment(params HtmlNode[] children) : base("shallow", children) { }
    public Fragment(IEnumerable<HtmlNode> children) : base("shallow", children:children) { }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// Because Shallow does not render itself, it does not take into account the indentation, and just passes it to its children.
    /// </remarks>
    public override string ToString(int indent = 0) => string.Join("", Children.Select(x => x.ToString(indent)));
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        foreach (var child in Children)
        {
            child.AppendTo(ref sb, indent);
            if(RenderOptions.Indent > 0)
                sb.AppendLine();
        }
    }
}