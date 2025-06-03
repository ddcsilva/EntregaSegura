/**
 * Constantes da aplicação
 * Centraliza todos os valores mágicos e configurações estáticas
 */

// Timeouts e intervalos (em minutos)
export const TIMEOUTS = {
  SESSION: 480,
  API_REQUEST: 30000,
  REFRESH_THRESHOLD: 30,
  RETRY_DELAY: 1000,
} as const;

// Limites de bundle e performance
export const BUNDLE_LIMITS = {
  INITIAL_WARNING: '500kb',
  INITIAL_ERROR: '1mb',
  COMPONENT_STYLE_WARNING: '2kb',
  COMPONENT_STYLE_ERROR: '4kb',
} as const;

// Configurações de paginação
export const PAGINATION = {
  DEFAULT_PAGE_SIZE: 10,
  MAX_PAGE_SIZE: 100,
  MIN_PAGE_SIZE: 5,
} as const;

// Configurações de validação
export const VALIDATION = {
  MIN_PASSWORD_LENGTH: 6,
  MAX_PASSWORD_LENGTH: 128,
  EMAIL_PATTERN: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
  PHONE_PATTERN: /^\(\d{2}\)\s\d{4,5}-\d{4}$/,
} as const;

// Chaves de storage
export const STORAGE_KEYS = {
  TOKEN: 'entrega_segura_token',
  USER_PREFERENCES: 'entrega_segura_preferences',
  THEME: 'entrega_segura_theme',
} as const;

// Configurações de retry
export const RETRY_CONFIG = {
  MAX_ATTEMPTS: 3,
  BACKOFF_BASE: 1000,
  BACKOFF_MULTIPLIER: 2,
} as const;

// Níveis de log
export const LOG_LEVELS = {
  ERROR: 'error',
  WARN: 'warn',
  INFO: 'info',
  DEBUG: 'debug',
} as const;

// Tipos para type safety
export type LogLevel = (typeof LOG_LEVELS)[keyof typeof LOG_LEVELS];
export type StorageKey = (typeof STORAGE_KEYS)[keyof typeof STORAGE_KEYS];
