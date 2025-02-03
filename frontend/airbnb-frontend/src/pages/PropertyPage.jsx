import { useParams } from "react-router-dom";
import styles from "../styles/PropertyPage.module.css";

const properties = [
  { id: 1, image: "https://source.unsplash.com/600x400/?house", title: "Уютный домик у озера", location: "Берлин, Германия", price: "120€", rating: 4.8, description: "Идеальное место для отдыха на природе с прекрасным видом на озеро." },
  { id: 2, image: "https://source.unsplash.com/600x400/?apartment", title: "Современные апартаменты", location: "Барселона, Испания", price: "90€", rating: 4.6, description: "Стильные апартаменты в центре Барселоны с видом на город." },
  { id: 3, image: "https://source.unsplash.com/600x400/?villa", title: "Вилла с бассейном", location: "Миконос, Греция", price: "250€", rating: 4.9, description: "Роскошная вилла с собственным бассейном и террасой." },
  { id: 4, image: "https://source.unsplash.com/600x400/?cabin", title: "Горная хижина", location: "Альпы, Швейцария", price: "180€", rating: 4.7, description: "Уютная хижина в горах для любителей активного отдыха." },
];

const PropertyPage = () => {
  const { id } = useParams();
  const property = properties.find((p) => p.id === Number(id));

  if (!property) {
    return <h2 className={styles.notFound}>Объект не найден 😞</h2>;
  }

  return (
    <div className={styles.propertyPage}>
      <img src={property.image} alt={property.title} className={styles.image} />
      <div className={styles.details}>
        <h1>{property.title}</h1>
        <p className={styles.location}>{property.location}</p>
        <p className={styles.price}>{property.price} / ночь</p>
        <p className={styles.rating}>⭐ {property.rating}</p>
        <p className={styles.description}>{property.description}</p>
        <button className={styles.bookButton}>Забронировать</button>
      </div>
    </div>
  );
};

export default PropertyPage;
