using LGym.Models;

namespace LGym.Services
{
    public interface ILGymRepository
    {
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member> GetMemberAsync(int id);
        Task<IEnumerable<Trainer>> GetTrainersAsync();
        Task<Trainer> GetTrainerAsync(int id);
        Task<IEnumerable<Session>> GetSessionsForTrainerAsync(int id);
        Task<IEnumerable<Session>> GetSessionsForMemberAsync(int id);

        // Challenge methods
        Task<Member> CreateMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Member member);

        Task<Trainer> CreateTrainerAsync(Trainer trainer);
        Task<Trainer> UpdateTrainerAsync(Trainer trainer);
    }
}
