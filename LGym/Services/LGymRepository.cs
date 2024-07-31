using LGym.DbContexts;
using LGym.Models;
using Microsoft.EntityFrameworkCore;

namespace LGym.Services
{
    public class LGymRepository:ILGymRepository
    {
        private readonly LGymContext _context;

        public LGymRepository(LGymContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<IEnumerable<Trainer>> GetTrainersAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<Trainer> GetTrainerAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<IEnumerable<Session>> GetSessionsForTrainerAsync(int id)
        {
            return await _context.Sessions
                .Where(s => s.TrainerId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetSessionsForMemberAsync(int id)
        {
            return await _context.Sessions
                .Where(s => s.MemberId == id)
                .ToListAsync();
        }

        // Challenge methods
        public async Task<Member> CreateMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var existingMember = await _context.Members.FindAsync(member.MemberId);
            if (existingMember != null)
            {
                existingMember.FirstName = member.FirstName;
                existingMember.LastName = member.LastName;
                existingMember.Email = member.Email;
                existingMember.JoinDate = member.JoinDate;

                await _context.SaveChangesAsync();
            }
            return existingMember;
        }

        public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer> UpdateTrainerAsync(Trainer trainer)
        {
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return trainer;
        }
    }
}