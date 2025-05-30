# Configurações de Segurança - EntregaSegura

## JWT (JSON Web Tokens)

### ⚠️ IMPORTANTE - Configuração de Produção

Antes de fazer deploy em produção, **OBRIGATORIAMENTE** altere as seguintes configurações:

#### 1. Chave JWT de Produção

No arquivo `appsettings.Production.json`, substitua a chave JWT por uma chave segura:

```json
{
  "Jwt": {
    "Key": "SUA_CHAVE_SUPER_SECRETA_AQUI_MINIMUM_32_CARACTERES_ALEATORIA",
    "Issuer": "EntregaSegura.API.Production",
    "Audience": "EntregaSegura.Client.Production",
    "ExpireHours": 24
  }
}
```

#### 2. Variáveis de Ambiente (Recomendado)

Para maior segurança, configure as chaves via variáveis de ambiente:

```bash
# Linux/macOS
export Jwt__Key="sua_chave_super_secreta_aqui"
export Jwt__Issuer="EntregaSegura.API.Production"
export Jwt__Audience="EntregaSegura.Client.Production"
export Jwt__ExpireHours="24"

# Windows
set Jwt__Key=sua_chave_super_secreta_aqui
set Jwt__Issuer=EntregaSegura.API.Production
set Jwt__Audience=EntregaSegura.Client.Production
set Jwt__ExpireHours=24
```

#### 3. Geração de Chave Segura

Para gerar uma chave JWT segura, use um dos métodos abaixo:

**PowerShell:**
```powershell
[Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes([System.Web.Security.Membership]::GeneratePassword(64, 10)))
```

**OpenSSL:**
```bash
openssl rand -base64 64
```

**Online (apenas para desenvolvimento):**
- https://generate-random.org/api-key-generator

### 🔒 Configurações de Segurança Implementadas

1. **Validação de Issuer e Audience**: Habilitada para produção
2. **HTTPS Obrigatório**: Habilitado para produção
3. **Validação de Tempo de Vida**: Habilitada
4. **Chaves com Mínimo de 32 Caracteres**: Validação implementada
5. **Claims Padronizadas**: Sub, Jti, Iat implementadas

### 🚫 O que NÃO fazer

- ❌ Nunca commite chaves de produção no código
- ❌ Não use chaves simples como "123456" ou "password"
- ❌ Não reutilize chaves entre ambientes
- ❌ Não desabilite HTTPS em produção

### ✅ Checklist de Deploy

- [ ] Chave JWT de produção configurada
- [ ] Issuer e Audience específicos de produção
- [ ] HTTPS configurado
- [ ] Variáveis de ambiente configuradas
- [ ] Logs de segurança habilitados
- [ ] Chaves antigas removidas do código

---

**Data da última atualização**: $(Get-Date -Format "yyyy-MM-dd")
**Versão**: 1.0