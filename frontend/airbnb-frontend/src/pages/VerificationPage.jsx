import { useState } from "react";
import styles from "../styles/VerificationPage.module.css";

const VerificationPage = () => {
  const [selectedFile, setSelectedFile] = useState(null);
  const [verificationStatus, setVerificationStatus] = useState("not_uploaded");

  const handleFileChange = (e) => {
    setSelectedFile(e.target.files[0]);
  };

  const handleUpload = () => {
    if (selectedFile) {
      setVerificationStatus("pending");
      setTimeout(() => {
        setVerificationStatus("verified");
      }, 3000); // Симуляция загрузки
    }
  };

  return (
    <div className={styles.container}>
      <h2>Верификация личности</h2>
      <p>Загрузите документ, подтверждающий вашу личность.</p>

      {verificationStatus === "not_uploaded" && (
        <>
          <input type="file" onChange={handleFileChange} />
          <button onClick={handleUpload}>Загрузить</button>
        </>
      )}

      {verificationStatus === "pending" && <p>Загрузка...</p>}

      {verificationStatus === "verified" && <p>✅ Документ проверен!</p>}
    </div>
  );
};

export default VerificationPage;
