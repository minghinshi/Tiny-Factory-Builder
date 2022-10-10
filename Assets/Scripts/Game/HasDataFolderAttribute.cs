using System;

public class HasDataFolderAttribute : Attribute
{
    private readonly string[] paths;

    public HasDataFolderAttribute(params string[] paths)
    {
        this.paths = paths;
    }

    public string[] GetPaths()
    {
        return paths;
    }
}
