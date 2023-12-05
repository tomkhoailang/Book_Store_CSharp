const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

const app = {
	filterData: { minVal: Infinity, maxVal: -Infinity, categories: [], page: 1, sort: "ReleaseYear", order: "asc" },

	navigate: function (controller, action, params) {
		const keys = Object?.keys(params);
		let urlString = `/${controller}/${action}`;

		if (keys?.length > 0) {
			const paramsString = keys.reduce((paramStr, key) => {
				paramStr.append(key, params[key]);
				return paramStr;
			}, new URLSearchParams());

			urlString += `?${paramsString}`;
		} 
		window.location.href = urlString;
	},

	submitFilterForm: async function () {
		_this = this;
		const data = await fetch("/BOOK_EDITION/Filter", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(this.filterData)
		});
		const wrapper = $("#_filtered-book-wrapper");
		const parseData = await data.text();
		wrapper.innerHTML = parseData;

		const scripts = wrapper.querySelectorAll('script');

		scripts.forEach(script => {
			const newScript = document.createElement('script');
			newScript.innerHTML = script.innerHTML;
			document.body.appendChild(newScript);
			script.parentNode.removeChild(script);
		});

		// page pagination
		$$(".page-pagination-btn").forEach(btn => {
			btn.onclick = function (e) {
				e.preventDefault();
				const clickedPage = this.getAttribute("page");
				_this.filterData.page = parseInt(clickedPage);
				_this.submitFilterForm();
			}
		})
	},

	handleEvents: function () {
		const _this = this;

		// price filter checked
		const prices = $$('#filter-by-prices input[type=checkbox]:not(:disabled)');

		prices.forEach((p) => {
			p.onchange = function (e) {
				const checkedPriceInputs = $$('#filter-by-prices input[type=checkbox]:not(:disabled):checked');

				if (Array.from(checkedPriceInputs).length > 0) {
					const matches = $(`#filter-by-prices label[for=${checkedPriceInputs[0].id}]`).textContent.trim().match(/\d{1,3}(?:\.\d{3})*(?:\.\d+)?/g);

					let minCheckedPrice = parseFloat(matches[0].replace(/\./g, ''));
					let maxCheckedPrice = parseFloat(matches[1].replace(/\./g, ''));

					for (let i of checkedPriceInputs) {
						const regex = /\d{1,3}(?:\.\d{3})*(?:\.\d+)?/g;
						const relativeLabel = $(`#filter-by-prices label[for=${i.id}]`);
						const priceRange = [];
						for (let m of relativeLabel.textContent.trim().match(regex)) {
							console.log(m)
							priceRange.push(parseFloat(m.replace(/\./g, '')));
						}
						_this.filterData.minVal = (priceRange[0] < minCheckedPrice) ? priceRange[0] : minCheckedPrice;
						_this.filterData.maxVal = (priceRange[1] > maxCheckedPrice) ? priceRange[1] : maxCheckedPrice;
					}
					console.log(_this.filterData)
				} else {
					_this.filterData.minVal = 0;
					_this.filterData.maxVal = Infinity;
				}
				_this.filterData.page = 1;
				_this.submitFilterForm();
			}
		});

		// categories filter checked
		const categories = $$('#filter-by-categories input[type=checkbox]:not(:disabled)');
		const checkedCategories = $$('#filter-by-categories input[type=checkbox]:not(:disabled):checked');

		// check if any checkbox have been checked
		for (let c of checkedCategories) {
			const categoryId = c.getAttribute("cate-id");
			_this.filterData.categories.push(categoryId);
		}

		categories.forEach((c) => {
			c.onchange = (e) => {
				const categoryId = c.getAttribute("cate-id");
				if (c.checked) {
					_this.filterData.categories.push(categoryId);
				} else {
					_this.filterData.categories = _this.filterData.categories.filter(cat => cat != categoryId);
				}
				_this.filterData.page = 1;
				_this.submitFilterForm();
			}
		})

		// Rating Handler
		const activeStar = "fa-solid fa-star .fa-star-rating";
		const inActiveStar = "far fa-star .fa-star-rating";
		const stars = [...$$(".fa-star-rating")];
		const starsLength = stars.length;
		let rating = 0;

		stars.forEach(s => {
			s.onclick = () => {
				let i = stars.indexOf(s);
				if (s.className === inActiveStar) {
					for (i; i >= 0; i--) {
						stars[i].className = activeStar;
					}
				} else {
					for (i; i < starsLength; i++) {
						stars[i].className = inActiveStar;
					}
				}
				rating = [...$$(".fa-solid.fa-star")].length;
				$("#ReviewRating").value = rating;
			}
		});

		// page pagination
		$$(".page-pagination-btn").forEach(btn => {
			btn.onclick = function (e) {
				e.preventDefault();
				const clickedPage = this.getAttribute("page");
				_this.filterData.page = parseInt(clickedPage);
				console.log(_this.filterData, clickedPage)
				_this.submitFilterForm();
			}
		})

		// sort button click
		$$(".sort-option").forEach(btn => {
			btn.onclick = () => {
				const sortOption = btn.getAttribute("sort-option");
				
				switch (sortOption) {
					case "lastest":
						_this.filterData.sort = "ReleaseYear";
						break;
					case "popularity":
						_this.filterData.sort = "Popularity";
						
						break;
					case "rating":
						_this.filterData.sort = "Rating";
						break;
					default:
						break;
				}
				console.log(_this.filterData)
				_this.submitFilterForm();
			};
		})

		// sort order button click
		$$(".sort-order").forEach(btn => {
			btn.onclick = () => {
				const sortOption = btn.getAttribute("sort-order");
				_this.filterData.order = sortOption;
				_this.submitFilterForm();
			};
		})
	},

	run: function (e) {
		this.handleEvents();
	}
}

document.addEventListener('DOMContentLoaded', app.run.bind(app));