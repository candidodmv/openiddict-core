import React from 'react';
import AuthService from '../services/authService';

export const PrivatePage = () => {

    const clickHandler = async () => {
        const service = new AuthService();
        const user = await service.getUser();
        console.log(user);
    }

    return (
        <div>
            Private page
            <button onClick={clickHandler}>Log User</button>
        </div>
    );
};
