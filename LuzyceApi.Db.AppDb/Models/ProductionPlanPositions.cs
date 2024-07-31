namespace LuzyceApi.Db.AppDb.Models;

public class ProductionPlanPositions
{
    public int Id { get; set; }
    public int ProductionPlanId { get; set; }
    public ProductionPlan? ProductionPlan { get; set; }
    public int DocumentPositionId { get; set; }
    public DocumentPositions? DocumentPosition { get; set; }
    public int? HeadsOfMetallurgicalTeamsId { get; set; }
    public User? HeadsOfMetallurgicalTeams { get; set; }
    public int? NumberOfHours { get; set; }
}