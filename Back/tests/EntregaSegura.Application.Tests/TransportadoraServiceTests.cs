using Moq;
using AutoMapper;
using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Interfaces;
using FluentAssertions;

namespace EntregaSegura.Application.Tests;

public class TransportadoraServiceTests
{
    private readonly Mock<ITransportadoraRepository> _transportadoraRepositoryMock;
    private readonly Mock<IEntregaRepository> _entregaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<INotificadorErros> _notificadorErrosMock;
    private readonly TransportadoraService _service;

    public TransportadoraServiceTests()
    {
        _transportadoraRepositoryMock = new Mock<ITransportadoraRepository>();
        _entregaRepositoryMock = new Mock<IEntregaRepository>();
        _mapperMock = new Mock<IMapper>();
        _notificadorErrosMock = new Mock<INotificadorErros>();

        _service = new TransportadoraService(
            _transportadoraRepositoryMock.Object,
            _entregaRepositoryMock.Object,
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
        _transportadoraRepositoryMock
            .Setup(r => r.BuscarPorCondicaoAsync(It.IsAny<Expression<Func<Transportadora, bool>>>(), false))
            .ReturnsAsync(new List<Transportadora>());
        _transportadoraRepositoryMock.Setup(r => r.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        await _service.AdicionarAsync(transportadoraDTO);

        // Assert
        _transportadoraRepositoryMock.Verify(r => r.Adicionar(transportadora), Times.Once);
        _transportadoraRepositoryMock.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Atualizar_Transportadora_Com_Sucesso()
    {
        // Arrange
        var transportadoraDTO = new TransportadoraDTO { Id = 1, Nome = "Transportadora", Cnpj = "22.264.404/0001-25", Telefone = "(11) 2345-6789", Email = "empresa@empresa.com.br" };
        var transportadora = new Transportadora("Transportadora", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br");

        _mapperMock.Setup(m => m.Map<Transportadora>(transportadoraDTO)).Returns(transportadora);
        _transportadoraRepositoryMock
            .Setup(r => r.BuscarPorIdAsync(transportadoraDTO.Id, false))
            .ReturnsAsync(transportadora);
        _transportadoraRepositoryMock.Setup(r => r.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        await _service.AtualizarAsync(transportadoraDTO);

        // Assert
        _transportadoraRepositoryMock.Verify(r => r.Atualizar(transportadora), Times.Once);
        _transportadoraRepositoryMock.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Remover_Transportadora_Com_Sucesso()
    {
        // Arrange
        var id = 1;
        var transportadora = new Transportadora("Transportadora", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br");

        _transportadoraRepositoryMock
            .Setup(r => r.BuscarPorIdAsync(id, true))
            .ReturnsAsync(transportadora);
        _transportadoraRepositoryMock.Setup(r => r.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        await _service.RemoverAsync(id);

        // Assert
        _transportadoraRepositoryMock.Verify(r => r.Remover(transportadora), Times.Once);
        _transportadoraRepositoryMock.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Obter_Todas_Transportadoras()
    {
        // Arrange
        var transportadoras = new List<Transportadora>
        {
            new Transportadora("Transportadora1", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br"),
            new Transportadora("Transportadora2", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br")
        };
        var transportadorasDTO = new List<TransportadoraDTO>
        {
            new TransportadoraDTO { Nome = "Transportadora1", Cnpj = "22.264.404/0001-25", Telefone = "(11) 2345-6789", Email = "empresa@empresa.com.br" },
            new TransportadoraDTO { Nome = "Transportadora2", Cnpj = "22.264.404/0001-25", Telefone = "(11) 2345-6789", Email = "empresa@empresa.com.br" }
        };

        _transportadoraRepositoryMock
            .Setup(r => r.BuscarTodosAsync(false))
            .ReturnsAsync(transportadoras);
        _mapperMock.Setup(m => m.Map<IEnumerable<TransportadoraDTO>>(transportadoras)).Returns(transportadorasDTO);

        // Act
        var result = await _service.ObterTodasTransportadorasAsync();

        // Assert
        result.Should().BeEquivalentTo(transportadorasDTO);
    }

    [Fact]
    public async Task Deve_Obter_Transportadora_Por_Id()
    {
        // Arrange
        var id = 1;
        var transportadora = new Transportadora("Transportadora", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br");
        var transportadoraDTO = new TransportadoraDTO { Id = id, Nome = "Transportadora", Cnpj = "22.264.404/0001-25", Telefone = "(11) 2345-6789", Email = "empresa@empresa.com.br" };

        _transportadoraRepositoryMock
            .Setup(r => r.BuscarPorIdAsync(id, true))
            .ReturnsAsync(transportadora);
        _mapperMock.Setup(m => m.Map<TransportadoraDTO>(transportadora)).Returns(transportadoraDTO);

        // Act
        var result = await _service.ObterTransportadoraPorIdAsync(id);

        // Assert
        result.Should().BeEquivalentTo(transportadoraDTO);
    }
}