using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloGGG.Models;

public class PostModel
{
    public PostModel()
    {
    }
    public int ID { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public string PostAuthor { get; set; }
    public string[] PostTags { get; set; }
    public string? OwnerID { get; set; }
    public IdentityUser? Owner { get; set; }
    [NotMapped] public string PostTagsString { get; set; }
}