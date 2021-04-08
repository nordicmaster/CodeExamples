document.querySelectorAll("div").forEach(x => x.animate([{transform:'rotate(360deg) translate3D(-50%,-40%,0)',color: '#000'},{color:'#432454',offset:0.3}],{duration:3000, iterations: Infinity}))
