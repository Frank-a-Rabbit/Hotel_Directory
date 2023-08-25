import React, { useState } from 'react';
import { TextField, Button, FormControl } from '@mui/material';
import styles from '../Styles/MainForm.module.css';

type MainFormProps = {
  children: React.ReactNode;
};

type AuthUser = {
  currentUser: {
    UserName?: string;
    Email?: string;
    Password?: string;
    ConfirmPassword?: string;
  };
}

interface FormData {
  UserName: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
}

const MainForm:React.FC<MainFormProps> = () => {
  const [authUser, setAuthUser] = useState<AuthUser>({ currentUser : {} });
  const [formData, setFormData] = useState<FormData>({
    UserName: '',
    Email: '',
    Password: '',
    ConfirmPassword: '',
  });
  const [pwordConfirmed, setPwordConfirmed] = useState(false);
  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

const handleSubmit = async (event: any) => {
  event.preventDefault();

  try {
    const res = await fetch("http://localhost:5043/assignroles", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(formData)
    });
    console.log("Farm data ", formData);
    if (res.ok) {
      console.log("Server responded with:", res);
      setAuthUser({ currentUser : await res.json()});
    } else {
      console.log("Server returned an error status:", res.status);
    }
  } catch (error) {
    console.error("An error occurred during the request:", error);
  }
};

const newFetch = () => {
  console.log('testing')
}

  return (
    <>
      <form onSubmit={handleSubmit}>
        <FormControl>
          <TextField
            label="UserName"
            name="UserName"
            value={formData.UserName}
            onChange={handleInputChange}
          />
        </FormControl>
        <FormControl>
          <TextField
            label="Email"
            name="Email"
            value={formData.Email}
            onChange={handleInputChange}
          />
        </FormControl>
        <FormControl>
          <TextField
            label="Password"
            name="Password"
            value={formData.Password}
            onChange={handleInputChange}
          />
        </FormControl>
        <FormControl>
          <TextField
            label="Confirm Password"
            name="ConfirmPassword"
            value={formData.ConfirmPassword}
            data-type='confirm'
            onChange={handleInputChange}
          />
        </FormControl>
        {formData.Password !== formData.ConfirmPassword && (
          <span>Passwords Do Not Match. Keep trying.</span>
        )}
        <Button type="submit" variant="contained" color="primary" className={pwordConfirmed ? styles.confirmed : ""}>
          Submit
        </Button>
      </form>
      <Button variant='contained' color='primary' onClick={newFetch}>
        Click Me
      </Button>
      {authUser.currentUser && (
        <div>{authUser.currentUser.UserName}</div>
      )}
    </>
  );
};

export default MainForm;
