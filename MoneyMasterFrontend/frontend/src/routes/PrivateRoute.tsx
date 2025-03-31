import React from 'react';
import { Navigate } from 'react-router-dom';
import authService from '../services/authService';

interface PrivateRouteProps {
    element: React.ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }: any) => {
    if (!authService.getToken()) {
        return <Navigate to="/login" />;
    }
    return <>{ element }</>
}

export default PrivateRoute;
