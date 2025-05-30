import { render, screen, fireEvent } from '@testing-library/angular';
import { ButtonComponent } from './button.component';

describe('ButtonComponent', () => {
  it('deve renderizar o botão com texto padrão', async () => {
    await render('<es-button>Clique aqui</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByRole('button', { name: /clique aqui/i });
    expect(button).toBeInTheDocument();
  });

  it('deve aplicar classes CSS baseadas na variant', async () => {
    await render('<es-button variant="success">Sucesso</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByTestId('es-button');
    expect(button).toHaveClass('bg-success-600');
  });

  it('deve emitir evento de click quando clicado', async () => {
    const mockClick = jest.fn();

    await render('<es-button (buttonClick)="onClick($event)">Clique</es-button>', {
      imports: [ButtonComponent],
      componentProperties: {
        onClick: mockClick,
      },
    });

    const button = screen.getByRole('button');
    fireEvent.click(button);

    expect(mockClick).toHaveBeenCalledTimes(1);
  });

  it('deve estar desabilitado quando prop disabled for true', async () => {
    await render('<es-button [disabled]="true">Desabilitado</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByRole('button');
    expect(button).toBeDisabled();
  });

  it('deve mostrar loading spinner quando loading for true', async () => {
    await render('<es-button [loading]="true">Carregando</es-button>', {
      imports: [ButtonComponent],
    });

    const spinner = screen.getByRole('button').querySelector('svg');
    expect(spinner).toBeInTheDocument();
    expect(spinner).toHaveClass('animate-spin');
  });
});
