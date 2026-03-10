import { render, screen, fireEvent } from '@testing-library/angular';
import { ButtonComponent } from './button.component';

describe('ButtonComponent', () => {
  it('deve renderizar o botão com texto padrão', async () => {
    await render('<es-button>Clique aqui</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByRole('button', { name: /clique aqui/i });
    expect(button).toBeTruthy();
  });

  it('deve aplicar classes CSS baseadas na variante', async () => {
    await render('<es-button variante="success">Sucesso</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByTestId('es-button');
    expect(button.className).toContain('bg-success-600');
  });

  it('deve emitir evento de click quando clicado', async () => {
    const mockClick = jest.fn();

    await render('<es-button (clique)="onClick($event)">Clique</es-button>', {
      imports: [ButtonComponent],
      componentProperties: {
        onClick: mockClick,
      },
    });

    const button = screen.getByRole('button');
    fireEvent.click(button);

    expect(mockClick).toHaveBeenCalledTimes(1);
  });

  it('deve estar desabilitado quando prop desabilitado for true', async () => {
    await render('<es-button [desabilitado]="true">Desabilitado</es-button>', {
      imports: [ButtonComponent],
    });

    const button = screen.getByRole('button') as HTMLButtonElement;
    expect(button.disabled).toBe(true);
  });

  it('deve mostrar loading spinner quando carregando for true', async () => {
    await render('<es-button [carregando]="true">Carregando</es-button>', {
      imports: [ButtonComponent],
    });

    const spinner = screen.getByRole('button').querySelector('svg');
    expect(spinner).toBeTruthy();

    const spinnerElement = spinner as SVGElement;
    expect(spinnerElement.getAttribute('class')).toContain('animate-spin');
  });
});
