using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatRepository _chatRepository;

    public ChatController(ChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages(string user1Id, string user2Id)
    {
        var messages = await _chatRepository.GetMessagesBetweenUsers(user1Id, user2Id);
        return Ok(messages);
    }
}
