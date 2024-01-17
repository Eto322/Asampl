using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using System.Diagnostics;
using System.Text;

public class RenderersKeyWordModel : AsamplKeyWordModel
{
    public List<RendererDataContainer> Renderers { get; set; }
    private bool IsDebug = true;

    public RenderersKeyWordModel()
    {
        Renderers = new List<RendererDataContainer>();
        KeyWordName = "RENDERERS";
        Description = "Block that defines rendering modules.";
    }

    // Додавання рендерера
    public bool AddRenderer(string rendererName, string path)
    {
        if (Renderers.Exists(r => r.DataName == rendererName))
        {
            Console.WriteLine($"Renderer '{rendererName}' already exists.");
            return false;
        }

        if (!File.Exists(path) && !IsDebug)
        {
            Console.WriteLine($"Renderer file not found: {path}");
            return false;
        }

        Renderers.Add(new RendererDataContainer(rendererName, path));
        return true;
    }

    // Видалення рендерера
    public bool RemoveRenderer(string rendererName)
    {
        return Renderers.RemoveAll(r => r.DataName == rendererName) > 0;
    }

    public string GetFormattedRenderers()
    {
        var formattedRenderers = new StringBuilder();
        formattedRenderers.AppendLine($"  {KeyWordName} {{");

        foreach (var renderer in Renderers)
        {
            formattedRenderers.AppendLine(renderer.GetDataRepresentation());
        }

        formattedRenderers.Append("}");

        return formattedRenderers.ToString();
    }

    public override List<AsamplKeyWordDataContainer> DataContainer
    {
        get
        {
            return Renderers.ConvertAll(x => (AsamplKeyWordDataContainer)x);
        }

        set
        {
            Renderers = value.ConvertAll(x => (RendererDataContainer)x);
        }
    }

    public override void Execute()
    {
        foreach (var renderer in Renderers)
        {
            Console.WriteLine($"Renderer: {renderer.DataName}, Path: {renderer.DataInfo}");
        }
    }

    public override string GetFormated()
    {
        return GetFormattedRenderers();
    }
}