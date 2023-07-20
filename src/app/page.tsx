import Post from './components/post'
import styles from './page.module.css'

export default function Home() {
  return (
    <main>
      <header>
        <a href="" className={styles.headerLinkLogo}>My Blog</a>
        <nav className={styles.headerNav}>
          <a href="">Login</a>
          <a href="">Register</a>
        </nav>
      </header>
      <Post />
    </main>
  )
}
