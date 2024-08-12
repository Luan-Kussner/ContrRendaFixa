using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services
{
    public class ContratanteService : IContratanteService
    {
        private readonly IContratanteRepository _contratanteRepository;

        public ContratanteService(IContratanteRepository contratanteRepository)
        {
            _contratanteRepository = contratanteRepository;
        }

        public async Task<IEnumerable<ContratanteGetViewModel>> GetContratantesAsync()
        {
            var contratantes = await _contratanteRepository.GetContratantesAsync();
            return contratantes.Select(c => new ContratanteGetViewModel(c));
        }

        public async Task<ContratanteGetViewModel> GetContratanteByIdAsync(int id)
        {
            var contratante = await _contratanteRepository.GetContratanteByIdAsync(id);
            if (contratante == null)
            {
                return null;
            }

            return new ContratanteGetViewModel(contratante);
        }

        public async Task<ContratanteModel> CreateContratanteAsync(ContratantePostViewModel contratanteViewModel)
        {
            if (string.IsNullOrEmpty(contratanteViewModel.Nome) || contratanteViewModel.Nome.Length < 10 || contratanteViewModel.Nome.Length > 150)
            {
                throw new ArgumentException(MensagensErrosViewModel.NomeInvalido);
            }

            if (string.IsNullOrEmpty(contratanteViewModel.Sobrenome) || contratanteViewModel.Sobrenome.Length < 10 || contratanteViewModel.Sobrenome.Length > 250)
            {
                throw new ArgumentException(MensagensErrosViewModel.SobrenomeInvalido);
            }

            var existingContratante = await _contratanteRepository.GetContratanteByNameAsync(contratanteViewModel.Nome);
            if (existingContratante != null)
            {
                throw new ArgumentException(MensagensErrosViewModel.ContratanteExistente);
            }

            var contratanteModel = new ContratanteModel
            {
                Nome = contratanteViewModel.Nome,
                Sobrenome = contratanteViewModel.Sobrenome,
                Segmento = contratanteViewModel.SegmentoEnumValue,
                Bloqueado = false
            };

            return await _contratanteRepository.CreateContratanteAsync(contratanteModel);
        }

        public async Task<bool> UpdateContratanteAsync(int id, ContratantePatchViewModel contratanteViewModel)
        {
            if (contratanteViewModel == null)
            {
                throw new ArgumentException(MensagensErrosViewModel.PatchErro);
            }

            var existingContratante = await _contratanteRepository.GetContratanteByIdAsync(id);
            if (existingContratante == null)
            {
                throw new KeyNotFoundException(MensagensErrosViewModel.ContratanteNotFound);
            }

            existingContratante.Bloqueado = contratanteViewModel.Bloqueado;

            return await _contratanteRepository.UpdateContratanteAsync(id, contratanteViewModel);
        }
    }
}
