export default function NotFound() {
  return (
    <section className="bg-white dark:bg-gray-900">
      <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
        <div className="mx-auto max-w-screen-sm text-center">
          <h1 className="mb-4 text-7xl tracking-tight font-extrabold lg:text-9xl text-dracula-pink dark:text-primary-500">404</h1>
          <p className="mb-4 text-3xl tracking-tight font-bold text-gray-900 md:text-4xl dark:text-white">Alguma coisa está faltando.</p>
          <p className="mb-4 text-lg font-light text-gray-500 dark:text-gray-400">Desculpe, não conseguimos encontrar essa página. Você encontrará muitas coisas para explorar na página inicial. </p>
          <a href="/" className="inline-flex text-white bg-dracula-pink hover:bg-dracula-pink-800 focus:ring-4 focus:outline-none focus:ring-dracula-pink font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:focus:ring-gray-400 my-4 transition-all duration-500 ease-in-out">Back to Homepage</a>
        </div>
      </div>
    </section>
  )
}