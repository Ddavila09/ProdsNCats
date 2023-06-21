#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdsNCats.Models;

public class Category
{
    [Key]
    public int CatId { get; set; }

    [Required(ErrorMessage ="is required.")]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /**********************************************************************
Relationship properties below

Foreign Keys: id of a different (foreign) model

Navigation props:
    Data type is a related model
    MUST use .Inlcude for the nav prop data to be included via a SQL JOIN.

***************************************************************************/

    public List<Association> AssProd { get; set; } = new List<Association>();

}