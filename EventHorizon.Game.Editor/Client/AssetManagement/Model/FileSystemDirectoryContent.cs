namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

using System;
using System.Collections.Generic;

public class FileSystemDirectoryContent
{
    public const string PATH_SEPARATOR = "/";

    public static string BuildPath(string rootPath, FileSystemDirectoryContent directoryContent)
    {
        var filterPath = directoryContent.FilterPath;
        if (directoryContent.IsFile)
        {
            return filterPath;
        }

        var path = directoryContent.Name;
        if (path == rootPath)
        {
            return path;
        }
        if (filterPath == rootPath)
        {
            return $"{rootPath}{path}";
        }
        var stringJoin = string.Join(PATH_SEPARATOR, filterPath, path);

        return stringJoin;
    }

    public string? Path { get; set; }

    public string? Action { get; set; }

    public string? NewName { get; set; }

    public string[]? Names { get; set; }

    public string Name { get; set; } = string.Empty;

    public long Size { get; set; }

    public string? PreviousName { get; set; }

    public DateTime DateModified { get; set; }

    public DateTime DateCreated { get; set; }

    public bool HasChild { get; set; }

    public bool IsFile { get; set; }

    public string? Type { get; set; }

    public string? Id { get; set; }

    public string FilterPath { get; set; } = string.Empty;

    public string? FilterId { get; set; }

    public string? ParentId { get; set; }

    public string? TargetPath { get; set; }

    public string[]? RenameFiles { get; set; }

    //public IList<IFormFile> UploadFiles { get; set; }

    public bool CaseSensitive { get; set; }

    public string? SearchString { get; set; }

    public bool ShowHiddenItems { get; set; }

    public FileSystemDirectoryContent[]? Data { get; set; }

    public FileSystemDirectoryContent? TargetData { get; set; }

    public AccessPermission? Permission { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is FileSystemDirectoryContent content
            && Path == content.Path
            && Action == content.Action
            && NewName == content.NewName
            && EqualityComparer<string[]>.Default.Equals(Names, content.Names)
            && Name == content.Name
            && Size == content.Size
            && PreviousName == content.PreviousName
            && DateModified == content.DateModified
            && DateCreated == content.DateCreated
            && HasChild == content.HasChild
            && IsFile == content.IsFile
            && Type == content.Type
            && Id == content.Id
            && FilterPath == content.FilterPath
            && FilterId == content.FilterId
            && ParentId == content.ParentId
            && TargetPath == content.TargetPath
            && EqualityComparer<string[]>.Default.Equals(RenameFiles, content.RenameFiles)
            && CaseSensitive == content.CaseSensitive
            && SearchString == content.SearchString
            && ShowHiddenItems == content.ShowHiddenItems
            && EqualityComparer<FileSystemDirectoryContent[]>.Default.Equals(Data, content.Data)
            && EqualityComparer<FileSystemDirectoryContent>.Default.Equals(
                TargetData,
                content.TargetData
            )
            && EqualityComparer<AccessPermission>.Default.Equals(Permission, content.Permission);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Path);
        hash.Add(Action);
        hash.Add(NewName);
        hash.Add(Names);
        hash.Add(Name);
        hash.Add(Size);
        hash.Add(PreviousName);
        hash.Add(DateModified);
        hash.Add(DateCreated);
        hash.Add(HasChild);
        hash.Add(IsFile);
        hash.Add(Type);
        hash.Add(Id);
        hash.Add(FilterPath);
        hash.Add(FilterId);
        hash.Add(ParentId);
        hash.Add(TargetPath);
        hash.Add(RenameFiles);
        hash.Add(CaseSensitive);
        hash.Add(SearchString);
        hash.Add(ShowHiddenItems);
        hash.Add(Data);
        hash.Add(TargetData);
        hash.Add(Permission);
        return hash.ToHashCode();
    }

    public static bool operator ==(
        FileSystemDirectoryContent? left,
        FileSystemDirectoryContent? right
    )
    {
        return EqualityComparer<FileSystemDirectoryContent>.Default.Equals(left, right);
    }

    public static bool operator !=(
        FileSystemDirectoryContent? left,
        FileSystemDirectoryContent? right
    )
    {
        return !(left == right);
    }
}
