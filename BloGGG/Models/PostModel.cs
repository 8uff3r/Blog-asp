using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloGGG.Models;

public class PostModel
{
    public int ID { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public string PostAuthor { get; set; }

    [NotMapped] public string PostTagsString { get; set; }
    public string[] PostTags { get; set; }
}