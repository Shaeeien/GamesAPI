import React, { useState, useEffect } from "react";

function Timer(){
    const[count, setCount] = useState(0);
    useEffect(() => {
        setTimeout(() => {
            setCount((count) => count + 1);
        }, 1000);
        }, []);
    return <h1>Wyrenderowano {count} razy!</h1>
}

export {Timer}