namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

public record ForgotPasswordResource(string Email);

public record ResetPasswordResource(string Token, string NewPassword, string ConfirmPassword);

public record VerifyEmailResource(string Token);

public record ResendVerificationResource(string Email);

public record VerifyPhoneResource(string Phone, string Code);

public record ResendPhoneVerificationResource(string Phone);
