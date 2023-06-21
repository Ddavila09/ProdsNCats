#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdsNCats.Models;

public class Association
{
    [Key]
    public int AssociationId { get; set; }

    /**********************************************************************
Relationship properties below

Foreign Keys: id of a different (foreign) model

Navigation props:
    Data type is a related model
    MUST use .Inlcude for the nav prop data to be included via a SQL JOIN.

***************************************************************************/

    public int ProductId { get; set; } //this foreign HAS TO MACH the PK property name
    public int CategoryId { get; set; } //this foreign HAS TO MACH the PK property name
    public Product? Product { get; set; }
    public Category? Category { get; set; }

}