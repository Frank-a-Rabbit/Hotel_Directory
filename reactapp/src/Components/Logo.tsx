import React from 'react';

type LogoProps = {
    url: string;
    alt: string;
}

const Logo:React.FC<LogoProps> = ({ url, alt }) => {
    return (
        <img loading='lazy' src={url} alt={alt} />
    )
}

export default Logo;