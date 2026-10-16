using Escola.Application.DTOs.Nota;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

namespace Escola.Application.Services
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _notaRepository;

        public NotaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        public async Task<NotaGetDTO> AddAsync(NotaPostDTO notaPostDTO)
        {
            var nota = new Nota
            {
                MatriculaId = notaPostDTO.MatriculaId,
                ValorNota = notaPostDTO.ValorNota,
                Aprovado = notaPostDTO.ValorNota >= 60,
                DataNota = DateTime.Now
            };
            
            var notaAdicionada = await _notaRepository.AddAsync(nota);

            return new NotaGetDTO
            {
                Id = notaAdicionada.Id,
                MatriculaId = notaAdicionada.MatriculaId,
                ValorNota = notaAdicionada.ValorNota,
                Aprovado = notaAdicionada.Aprovado,
                DataNota = notaAdicionada.DataNota
            };
        }

        public async Task<NotaGetDTO> DeleteAsync(int id)
        {
            var notaDeletada = await _notaRepository.DeleteAsync(id);

            if(notaDeletada == null)
            {
                return null;
            }

            return new NotaGetDTO
            {
                Id = notaDeletada.Id,
                MatriculaId = notaDeletada.MatriculaId,
                ValorNota = notaDeletada.ValorNota,
                Aprovado = notaDeletada.Aprovado,
                DataNota = notaDeletada.DataNota
            };
        }

        public async Task<List<NotaGetDTO>> GetAllAsync()
        {
            var notas = await _notaRepository.GetAllAsync();
            var notasDTO = new List<NotaGetDTO>();

            foreach (var nota in notas)
            {
                notasDTO.Add(new NotaGetDTO
                {
                    Id = nota.Id,
                    MatriculaId = nota.MatriculaId,
                    ValorNota = nota.ValorNota,
                    Aprovado = nota.Aprovado,
                    DataNota = nota.DataNota
                });
            }
            return notasDTO;
        }

        public async Task<NotaGetDTO> GetByIdAsync(int id)
        {
            var nota = await _notaRepository.GetByIdAsync(id);

            if(nota == null)
            {
                return null;
            }

            return new NotaGetDTO
            {
                Id = nota.Id,
                MatriculaId = nota.MatriculaId,
                ValorNota = nota.ValorNota,
                Aprovado = nota.Aprovado,
                DataNota = nota.DataNota
            };
        }

        public async Task<NotaGetDTO> UpdateAsync(NotaPutDTO notaPutDTO)
        {
            var nota = await _notaRepository.GetByIdAsync(notaPutDTO.Id);

            if(nota == null)
            {
                return null;
            }

            nota.MatriculaId = notaPutDTO.MatriculaId;
            nota.ValorNota = notaPutDTO.ValorNota;
            nota.Aprovado = notaPutDTO.ValorNota >= 60;
            nota.DataNota = DateTime.Now;

            var notaAtualizada = await _notaRepository.UpdateAsync(nota);

            return new NotaGetDTO
            {
                Id = notaAtualizada.Id,
                MatriculaId = notaAtualizada.MatriculaId,
                ValorNota = notaAtualizada.ValorNota,
                Aprovado = notaAtualizada.Aprovado,
                DataNota = notaAtualizada.DataNota
            };
        }
    }
}
