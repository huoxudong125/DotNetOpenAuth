﻿//-----------------------------------------------------------------------
// <copyright file="RelyingPartySecuritySettings.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.OpenId.RelyingParty {
	using System;
	using DotNetOpenAuth.Messaging;

	/// <summary>
	/// Security settings that are applicable to relying parties.
	/// </summary>
	public sealed class RelyingPartySecuritySettings : SecuritySettings {
		/// <summary>
		/// Initializes a new instance of the <see cref="RelyingPartySecuritySettings"/> class.
		/// </summary>
		internal RelyingPartySecuritySettings()
			: base(false) {
			this.PrivateSecretMaximumAge = TimeSpan.FromDays(7);
		}

		/// <summary>
		/// Gets or sets a value indicating whether the entire pipeline from Identifier discovery to 
		/// Provider redirect is guaranteed to be encrypted using HTTPS for authentication to succeed.
		/// </summary>
		/// <remarks>
		/// <para>Setting this property to true is appropriate for RPs with highly sensitive 
		/// personal information behind the authentication (money management, health records, etc.)</para>
		/// <para>When set to true, some behavioral changes and additional restrictions are placed:</para>
		/// <list>
		/// <item>User-supplied identifiers lacking a scheme are prepended with
		/// HTTPS:// rather than the standard HTTP:// automatically.</item>
		/// <item>User-supplied identifiers are not allowed to use HTTP for the scheme.</item>
		/// <item>All redirects during discovery on the user-supplied identifier must be HTTPS.</item>
		/// <item>Any XRDS file found by discovery on the User-supplied identifier must be protected using HTTPS.</item>
		/// <item>Only Provider endpoints found at HTTPS URLs will be considered.</item>
		/// <item>If the discovered identifier is an OP Identifier (directed identity), the 
		/// Claimed Identifier eventually asserted by the Provider must be an HTTPS identifier.</item>
		/// <item>In the case of an unsolicited assertion, the asserted Identifier, discovery on it and 
		/// the asserting provider endpoint must all be secured by HTTPS.</item>
		/// </list>
		/// <para>Although the first redirect from this relying party to the Provider is required
		/// to use HTTPS, any additional redirects within the Provider cannot be protected and MAY
		/// revert the user's connection to HTTP, based on individual Provider implementation.
		/// There is nothing that the RP can do to detect or prevent this.</para>
		/// <para>
		/// A <see cref="ProtocolException"/> is thrown during discovery or authentication when a secure pipeline cannot be established.
		/// </para>
		/// </remarks>
		public bool RequireSsl { get; set; }

		/// <summary>
		/// Gets or sets the oldest version of OpenID the remote party is allowed to implement.
		/// </summary>
		/// <value>Defaults to <see cref="ProtocolVersion.V10"/></value>
		public ProtocolVersion MinimumRequiredOpenIdVersion { get; set; }

		/// <summary>
		/// Gets or sets the maximum allowable age of the secret a Relying Party
		/// uses to its return_to URLs and nonces with 1.0 Providers.
		/// </summary>
		/// <value>The default value is 7 days.</value>
		public TimeSpan PrivateSecretMaximumAge { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether all unsolicited assertions should be ignored.
		/// </summary>
		/// <value>The default value is <c>false</c>.</value>
		public bool RejectUnsolicitedAssertions { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether delegating identifiers are refused for authentication.
		/// </summary>
		/// <value>The default value is <c>false</c>.</value>
		/// <remarks>
		/// When set to <c>true</c>, login attempts that start at the RP or arrive via unsolicited
		/// assertions will be rejected if discovery on the identifier shows that OpenID delegation
		/// is used for the identifier.  This is useful for an RP that should only accept identifiers
		/// directly issued by the Provider that is sending the assertion.
		/// </remarks>
		public bool RejectDelegatingIdentifiers { get; set; }
	}
}