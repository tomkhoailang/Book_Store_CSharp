const toasts = document.querySelector('#toasts');

const createToast = (type = 'success', message = 'This is a message !', time = 3) => {
    let toast = document.createElement('div');
    let icon = "";
    toast.setAttribute('class', `custom-toast ${type}`);

    if (type === 'success') {
        icon = '<i class="fa-solid fa-circle-check"></i>'
    } else if (type === 'warning') {
        icon = '<i class="fa-solid fa-circle-exclamation"></i>';
    } else if (type === 'error') {
        icon = '<i class="fa-solid fa-triangle-exclamation"></i>';
    }
    
    toast.innerHTML = `
                        ${icon}
                            <span class="text-light">${message}</span>
                        <div class="countdown"></div>
                      `;
    toast.style.animation = `slide-in ease 1.5s forwards, slide-out ease-in 1s forwards ${time}s`;
    toast.querySelector('.countdown').style.animation = `countdown linear ${time}s forwards`;
    toasts.appendChild(toast);
    console.log(toast)
    setTimeout(() => {
        toast.remove()
    }, (time + 1) * 1000);
}