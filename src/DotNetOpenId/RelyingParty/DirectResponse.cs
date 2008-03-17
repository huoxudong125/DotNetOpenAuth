﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetOpenId.RelyingParty {
	class DirectResponse {
		protected DirectResponse(Uri provider, IDictionary<string, string> args) {
			if (provider == null) throw new ArgumentNullException("provider");
			if (args == null) throw new ArgumentNullException("args");
			Provider = provider;
			Args = args;
		}
		protected Uri Provider { get; private set; }
		protected IDictionary<string, string> Args { get; private set; }
	}
}
