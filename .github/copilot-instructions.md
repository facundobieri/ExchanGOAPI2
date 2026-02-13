# Copilot Instructions

## General Guidelines
- Use concise and actionable language in your comments and code.
- Ensure that your code is well-structured and follows best practices.
- Prefer inline mapping or have repositories return DTOs instead of using a separate mapping layer. However, if a mapping layer is necessary, implement it for mapping `CreateUserRequest <-> User` and `User -> UserDto`. Additionally, prefer using the `DTO.UpdateEntity(user)` extension method for updating entities to keep mapping consistent with `CreateUserRequest.ToEntity` and `User.ToDto`.
- Passwords must be persisted hashed (never plaintext) and the project will use JWT for authentication; apply password hashing and JWT rules in services and authentication flow.
- Role changes must only be performed via direct database/hard rewrite; the API must not allow role updates. This ensures that roles are modified only by direct database/hard rewrite by the user.

## Code Style
- Follow consistent naming conventions for classes and interfaces.
- Use specific formatting rules to maintain readability.

## Project-Specific Rules
- When working with repositories, ensure that interfaces like `IBaseRepository`, `ICurrencyRepository`, and `IUserRepository` are implemented correctly.
- Focus on the `User` entity in `Domain\Entities\User.cs` to ensure it adheres to the defined interfaces and business logic.
- Do not use `CancellationToken` in repository or service method signatures.

## Objetive
- The user can create an account and log in
- The system allows to CRUD currencies, but only admin users can create and delete currencies. Regular users can only read them.
- The user without suscrpition can't create conversions, the free user can make up to 10 conversions, the trial can make up to 100 and the pro user can make unlimited conversions.
- The conversion is made by the user selecting two currencies and the amount to convert, then the system will return the converted amount based on the current exchange rate.