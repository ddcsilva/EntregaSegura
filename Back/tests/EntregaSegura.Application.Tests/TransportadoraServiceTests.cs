using Moq;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.UnitOfWork;
using AutoMapper;
using System.Linq.Expressions;

namespace EntregaSegura.Application.Tests
{
    public class TransportadoraServiceTests
    {
        private readonly Mock<ITransportadoraRepository> _transportadoraRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<INotificadorErros> _notificadorErrosMock;
        private readonly TransportadoraService _service;

        public TransportadoraServiceTests()
        {
            _transportadoraRepositoryMock = new Mock<ITransportadoraRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _notificadorErrosMock = new Mock<INotificadorErros>();

            _service = new TransportadoraService(
                _transportadoraRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _notificadorErrosMock.Object);
        }

        [Fact]
        public async Task Deve_Adicionar_Transportadora_Com_Sucesso()
        {
            // Arrange
            var transportadoraDTO = new TransportadoraDTO { Nome = "Transportadora", Cnpj = "22.264.404/0001-25", Telefone = "(11) 2345-6789", Email = "empresa@empresa.com.br" };
            var transportadora = new Transportadora("Transportadora", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br");

            _mapperMock.Setup(m => m.Map<Transportadora>(transportadoraDTO)).Returns(transportadora);
            _transportadoraRepositoryMock.Setup(r => r.BuscarAsync(It.IsAny<Expression<Func<Transportadora, bool>>>())).ReturnsAsync(new List<Transportadora>());
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            // Act
            await _service.AdicionarAsync(transportadoraDTO);

            // Assert
            _transportadoraRepositoryMock.Verify(r => r.Adicionar(transportadora), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
