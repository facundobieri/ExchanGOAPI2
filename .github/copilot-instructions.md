# Copilot Instructions

## General Guidelines
- Use concise and actionable language in your comments and code.
- Ensure that your code is well-structured and follows best practices.

## Code Style
- Follow consistent naming conventions for classes and interfaces.
- Use specific formatting rules to maintain readability.

## Project-Specific Rules
- When working with repositories, ensure that interfaces like `IBaseRepository`, `ICurrencyRepository`, and `IUserRepository` are implemented correctly.
- Focus on the `User` entity in `Domain\Entities\User.cs` to ensure it adheres to the defined interfaces and business logic.
- Do not use `CancellationToken` in repository or service method signatures.