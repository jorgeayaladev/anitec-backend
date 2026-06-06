using Titan.AniTec.Platform.Communication.Application.CommandServices;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Communication.Interfaces.Assemblers;
using Titan.AniTec.Platform.Communication.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Communication.Interfaces.Controllers;

[ApiController]
[Route("api/communication")]
[Authorize]
public class CommunicationController(
    ICommunicationCommandService commandService,
    ICommunicationQueryService queryService) : ControllerBase
{
    private int CurrentFarmId => ((User)HttpContext.Items["User"]!).Id;

    // Conversations
    [HttpGet("conversations")]
    public async Task<IActionResult> GetConversations()
    {
        var query = new GetConversationsQuery(CurrentFarmId);
        var result = await queryService.GetConversationsAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpGet("conversations/{conversationId:int}")]
    public async Task<IActionResult> GetConversation(int conversationId)
    {
        var query = new GetConversationByIdQuery(CurrentFarmId, conversationId);
        var result = await queryService.GetConversationByIdAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPost("conversations")]
    public async Task<IActionResult> CreateConversation([FromBody] CreateConversationResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.CreateConversationAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpGet("conversations/{conversationId:int}/messages")]
    public async Task<IActionResult> GetMessages(int conversationId)
    {
        var query = new GetConversationMessagesQuery(CurrentFarmId, conversationId);
        var result = await queryService.GetConversationMessagesAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPost("conversations/{conversationId:int}/messages")]
    public async Task<IActionResult> SendMessage(int conversationId, [FromBody] SendMessageResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, conversationId, resource);
        var result = await commandService.SendMessageAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("messages/{messageId:int}/read")]
    public async Task<IActionResult> MarkMessageAsRead(int messageId)
    {
        var command = new MarkMessageAsReadCommand(CurrentFarmId, messageId);
        var result = await commandService.MarkMessageAsReadAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpDelete("messages/{messageId:int}")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var command = new DeleteMessageCommand(CurrentFarmId, messageId);
        var result = await commandService.DeleteMessageAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var query = new GetUnreadCountQuery(CurrentFarmId);
        var result = await queryService.GetUnreadCountAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(count => new UnreadCountResource(count)));
    }

    [HttpPut("conversations/{conversationId:int}/read-all")]
    public async Task<IActionResult> MarkAllAsRead(int conversationId)
    {
        var command = new MarkAllAsReadCommand(CurrentFarmId, conversationId);
        var result = await commandService.MarkAllAsReadAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("conversations/{conversationId:int}/archive")]
    public async Task<IActionResult> ArchiveConversation(int conversationId)
    {
        var command = new ArchiveConversationCommand(CurrentFarmId, conversationId);
        var result = await commandService.ArchiveConversationAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    // Shared Reports
    [HttpPost("reports/share/{reportId:int}")]
    public async Task<IActionResult> ShareReport(int reportId, [FromBody] ShareReportResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, reportId, resource);
        var result = await commandService.ShareReportAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpGet("reports/shared")]
    public async Task<IActionResult> GetSharedReports()
    {
        var query = new GetSharedReportsQuery(CurrentFarmId);
        var result = await queryService.GetSharedReportsAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpGet("reports/shared-by-me")]
    public async Task<IActionResult> GetSharedByMeReports()
    {
        var query = new GetSharedByMeReportsQuery(CurrentFarmId);
        var result = await queryService.GetSharedByMeReportsAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpDelete("reports/share/{shareId:int}")]
    public async Task<IActionResult> RevokeSharedReport(int shareId)
    {
        var command = new RevokeSharedReportCommand(CurrentFarmId, shareId);
        var result = await commandService.RevokeSharedReportAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reports/{shareId:int}/view")]
    public async Task<IActionResult> ViewSharedReport(int shareId)
    {
        var query = new GetSharedReportViewQuery(CurrentFarmId, shareId);
        var result = await queryService.GetSharedReportViewAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    // Push Notifications
    [HttpPost("notifications/register-device")]
    public async Task<IActionResult> RegisterPushDevice([FromBody] RegisterPushDeviceResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.RegisterPushDeviceAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpDelete("notifications/unregister-device/{deviceId:int}")]
    public async Task<IActionResult> UnregisterPushDevice(int deviceId)
    {
        var command = new UnregisterPushDeviceCommand(CurrentFarmId, deviceId);
        var result = await commandService.UnregisterPushDeviceAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("notifications/settings")]
    public async Task<IActionResult> GetNotificationSettings()
    {
        var query = new GetNotificationSettingsQuery(CurrentFarmId);
        var result = await queryService.GetNotificationSettingsAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("notifications/settings")]
    public async Task<IActionResult> UpdateNotificationSettings([FromBody] UpdateNotificationSettingsResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.UpdateNotificationSettingsAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    // Contacts
    [HttpGet("contacts")]
    public async Task<IActionResult> GetContacts()
    {
        var query = new GetContactsQuery(CurrentFarmId);
        var result = await queryService.GetContactsAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPost("contacts")]
    public async Task<IActionResult> AddContact([FromBody] CreateContactResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.AddContactAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("contacts/{contactId:int}")]
    public async Task<IActionResult> UpdateContact(int contactId, [FromBody] UpdateContactResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, contactId, resource);
        var result = await commandService.UpdateContactAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpDelete("contacts/{contactId:int}")]
    public async Task<IActionResult> DeleteContact(int contactId)
    {
        var command = new DeleteContactCommand(CurrentFarmId, contactId);
        var result = await commandService.DeleteContactAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("contacts/search")]
    public async Task<IActionResult> SearchContacts([FromQuery] string q)
    {
        var query = new GetContactsSearchQuery(CurrentFarmId, q);
        var result = await queryService.GetContactsSearchAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpGet("contacts/veterinarians")]
    public async Task<IActionResult> GetVeterinarians()
    {
        var query = new GetVeterinariansQuery(CurrentFarmId);
        var result = await queryService.GetVeterinariansAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpGet("contacts/farmers")]
    public async Task<IActionResult> GetFarmers()
    {
        var query = new GetFarmersQuery(CurrentFarmId);
        var result = await queryService.GetFarmersAsync(query);
        return CommunicationActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPost("contacts/request")]
    public async Task<IActionResult> SendContactRequest([FromBody] SendContactRequestResource resource)
    {
        var command = CommunicationAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.SendContactRequestAsync(command);
        return CommunicationActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("contacts/request/{requestId:int}/accept")]
    public async Task<IActionResult> AcceptContactRequest(int requestId)
    {
        var command = new AcceptContactRequestCommand(CurrentFarmId, requestId);
        var result = await commandService.AcceptContactRequestAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("contacts/request/{requestId:int}/reject")]
    public async Task<IActionResult> RejectContactRequest(int requestId)
    {
        var command = new RejectContactRequestCommand(CurrentFarmId, requestId);
        var result = await commandService.RejectContactRequestAsync(command);
        return CommunicationActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }
}
