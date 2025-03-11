import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "../styles/SignInForm.module.css";
import SocialSignIn from "./SocialSignIn";

const SignInForm = ({ onClose, toggleAuth }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await loginUser(email, password);
      onClose();
      navigate("/profile");
    } catch (error) {
      alert(error.message);
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>Войти</h2>
      <input type="text" placeholder="Логин" value={username} onChange={(e) => setUsername(e.target.value)} required />
      <input type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)} required />
      <button type="submit" className={styles.submitBtn}>Войти</button>

      <div className={styles.orDivider}>или</div>
      <SocialSignIn />

      <p className={styles.registerLink}>
        Нет аккаунта? <span onClick={toggleAuth}>Зарегистрируйтесь</span>
      </p>
    </form>
  );
};

export default SignInForm;
