#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdsNCats.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage ="is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage ="is required.")]
    
    public string Description  { get; set; }

    [Required(ErrorMessage ="is required.")]
    [Range(0,5000)]
    public int? Price { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /**********************************************************************
Relationship properties below

Foreign Keys: id of a different (foreign) model

Navigation props:
    Data type is a related model
    MUST use .Inlcude for the nav prop data to be included via a SQL JOIN.

***************************************************************************/

    public List<Association> AssCat { get; set; } = new List<Association>();

}