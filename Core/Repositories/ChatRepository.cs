using Microsoft.EntityFrameworkCore;

public class ChatRepository
{
    private readonly ApplicationDbContext _context;

    public ChatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveMessageAsync(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetMessagesBetweenUsers(string user1Id, string user2Id)
    {
        return await _context.ChatMessages
            .Where(m => (m.FromUserId == user1Id && m.ToUserId == user2Id) ||
                        (m.FromUserId == user2Id && m.ToUserId == user1Id))
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }
}
