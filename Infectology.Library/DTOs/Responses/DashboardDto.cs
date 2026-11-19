namespace Infectology.Library.DTOs.Responses;

public record DashboardDto(
    int NewDocuments,
    int UpcomingEvents,
    int NewDiscussions,
    int Announcements
);