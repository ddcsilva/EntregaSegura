module.exports = {
  preset: "jest-preset-angular",
  setupFilesAfterEnv: ["<rootDir>/setup-jest.ts"],
  testPathIgnorePatterns: ["<rootDir>/node_modules/", "<rootDir>/dist/"],
  collectCoverage: true,
  coverageDirectory: "coverage",
  coverageReporters: ["html", "text-summary", "lcov"],
  coverageThreshold: {
    global: {
      branches: 80,
      functions: 80,
      lines: 80,
      statements: 80,
    },
  },
  // 🚀 Performance otimizada
  cache: true,
  maxWorkers: "50%",
  // 📂 Path mapping (mesmo do tsconfig.json)
  moduleNameMapper: {
    "^@core/(.*)$": "<rootDir>/src/app/core/$1",
    "^@shared/(.*)$": "<rootDir>/src/app/shared/$1",
    "^@ui/(.*)$": "<rootDir>/src/app/ui/$1",
    "^@layout/(.*)$": "<rootDir>/src/app/layout/$1",
    "^@features/(.*)$": "<rootDir>/src/app/features/$1",
    "^@environments/(.*)$": "<rootDir>/src/environments/$1",
  },
};
