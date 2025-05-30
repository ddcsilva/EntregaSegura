import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export type ButtonVariant = 'primary' | 'secondary' | 'success' | 'warning' | 'danger';
export type ButtonSize = 'sm' | 'md' | 'lg';

@Component({
  selector: 'es-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
})
export class ButtonComponent {
  @Input() variant: ButtonVariant = 'primary';
  @Input() size: ButtonSize = 'md';
  @Input() disabled = false;
  @Input() loading = false;
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
  @Input() fullWidth = false;

  @Output() buttonClick = new EventEmitter<Event>();

  get buttonClasses(): string {
    const base =
      'inline-flex items-center justify-center font-medium rounded-lg focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-200';

    const variants = {
      primary: 'bg-primary-600 hover:bg-primary-700 focus:ring-primary-500 text-white',
      secondary: 'bg-white hover:bg-gray-50 focus:ring-primary-500 text-gray-700 border border-gray-300',
      success: 'bg-success-600 hover:bg-success-700 focus:ring-success-500 text-white',
      warning: 'bg-warning-600 hover:bg-warning-700 focus:ring-warning-500 text-white',
      danger: 'bg-danger-600 hover:bg-danger-700 focus:ring-danger-500 text-white',
    };

    const sizes = {
      sm: 'px-3 py-1.5 text-sm',
      md: 'px-4 py-2 text-base',
      lg: 'px-6 py-3 text-lg',
    };

    const width = this.fullWidth ? 'w-full' : '';

    return [base, variants[this.variant], sizes[this.size], width].filter(Boolean).join(' ');
  }

  handleClick(event: Event): void {
    if (!this.disabled && !this.loading) {
      this.buttonClick.emit(event);
    }
  }
}
