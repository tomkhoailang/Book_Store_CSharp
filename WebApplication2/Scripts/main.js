'use-strict'

const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

class EventEmitter {
	constructor() {
		this.eventsMap = new Map();
	}

	on(eventName, registerCallback) {
		if (!this.eventsMap.has(eventName)) {
			this.eventsMap.set(eventName, [registerCallback]);
		} else {
			this.eventsMap.get(eventName).push(registerCallback);
		}
	}

	emit(eventName, ...params) {
		if (!this.eventsMap.has(eventName)) {
			return;
		}
		this.eventsMap.get(eventName).forEach((cb) => {
			cb(...params);
		})
	}
}

const app = {
	filterData: { minVal: Infinity, maxVal: -Infinity, categories: [] },

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

	submitFilterForm: async function () {
		const data = await fetch("/BOOK_EDITION/FilteredBook", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(this.filterData)
		});
		const wrapper = $("#_filtered-book-wrapper");
		const parseData = await data.text();
		wrapper.innerHTML = parseData;
	},

	handleEvents: function () {
		const _this = this;

		// price filter checked
		const prices = $$('#filter-by-prices input[type=checkbox]:not(:disabled)');

		prices.forEach((p) => {
			p.onchange = function (e) {
				const checkedPriceInputs = $$('#filter-by-prices input[type=checkbox]:not(:disabled):checked');

				if (Array.from(checkedPriceInputs).length > 0) {
					let minCheckedPrice = parseInt([...$(`#filter-by-prices label[for=${checkedPriceInputs[0].id}]`).textContent.trim().matchAll(/\$(\d+)\b/g)][0][1]);
					let maxCheckedPrice = parseInt([...$(`#filter-by-prices label[for=${checkedPriceInputs[0].id}]`).textContent.trim().matchAll(/\$(\d+)\b/g)][1][1]);

					for (let i of checkedPriceInputs) {
						const regex = /\$(\d+)\b/g;
						const relativeLabel = $(`#filter-by-prices label[for=${i.id}]`);
						const priceRange = [];
						for (let m of relativeLabel.textContent.trim().matchAll(regex)) {
							priceRange.push(parseInt(m[1]));
						}
						_this.filterData.minVal = (priceRange[0] < minCheckedPrice) ? priceRange[0] : minCheckedPrice;
						_this.filterData.maxVal = (priceRange[1] > maxCheckedPrice) ? priceRange[1] : maxCheckedPrice;
					}
				} else {
					_this.filterData.minVal = 0;
					_this.filterData.maxVal = Infinity;
				}
				
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
				console.log(_this.filterData)

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
			}
		});

		const ratingForm = $("#rating-form");

		ratingForm?.onsubmit = (e) => {
			e.preventDefault();
			const ratingTextarea = $("#ReviewDescription");

			if (ratingTextarea.value.trim() === "") {
				alert("Vui lòng viết đánh giá");
				return;
			};
			if (rating === 0) {
				alert("Vui lòng cho đánh giá");
				return;
			};

			$("#ReviewRating").value = rating;

			ratingForm.submit();
		}

	},

	run: function (e) {
		this.handleEvents();
	}
}

document.addEventListener('DOMContentLoaded', app.run.bind(app));
