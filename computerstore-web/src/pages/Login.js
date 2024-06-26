import React from 'react';
import { useNavigate } from 'react-router-dom';

function Login({ login }) {
    const navigate = useNavigate();

    const handleLogin = () => {
        login();
        navigate('/user');
    };

    return (
        <div className="container">
            <h2>Login</h2>
            <button onClick={handleLogin}>Login</button>
        </div>
    );
}

export default Login;