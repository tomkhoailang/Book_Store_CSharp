'use-strict'

const app = {


	navigate: function (controller, action, params) {
		const keys = Object.keys(params);
		let urlString = `/${controller}/${action}`;

		if (keys.length > 0) {
			const paramsString = keys.reduce((paramStr, key) => {
				paramStr.append(key, params[key]);
				return paramStr;
			}, new URLSearchParams());

			urlString += `?${paramsString}`;
		}
		window.location.href = urlString;
	},

	handleEvents: function () {
		
	},

	run: function (e) {
		this.handleEvents();
	}
}

document.addEventListener('DOMContentLoaded', app.run.bind(app));
