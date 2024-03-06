import { useState } from 'react'
import { subscribeNewsLetter } from '../utils/api'
import { toast } from 'react-toastify'

const API_URL = process.env.NEXT_PUBLIC_API_URL

export default function NewsLetter() {
  const [email, setEmail] = useState<string>('')

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    try {
      await subscribeNewsLetter(email)
      toast.success('Assinatura bem-sucedida!', {
        className:
          'bg-white dark:bg-gray-900 text-dracula-purple hover:underline dark:text-dracula-purple',
      })
    } catch (error) {
      toast.error(
        'Falha ao se inscrever no newsletter. Tente novamente mais tarde.',
      )
    }
    setEmail('')
  }

  return (
    <section className='bg-white dark:bg-gray-900'>
      <div className='mx-auto max-w-screen-xl px-4 py-8 lg:px-6 lg:py-16'>
        <div className='mx-auto max-w-screen-md sm:text-center'>
          <h2 className='mb-4 text-3xl font-extrabold tracking-tight text-gray-900 dark:text-white sm:text-4xl'>
            Newsletter
          </h2>
          <p className='mx-auto mb-8 max-w-2xl  text-gray-500 dark:text-gray-400 sm:text-xl md:mb-12'>
            Para manter-se atualizado sobre novos artigos, assine com seu
            e-mail.
          </p>
          <form onSubmit={handleSubmit}>
            <div className='mx-auto mb-3 max-w-screen-sm items-center space-y-4 sm:flex sm:space-y-0'>
              <div className='relative w-full'>
                <label
                  htmlFor='email'
                  className='mb-2 hidden text-sm font-medium text-gray-900 dark:text-gray-300'
                >
                  Endereço de e-mail
                </label>
                <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
                  <svg
                    className='h-4 w-4 text-gray-500 dark:text-gray-400'
                    aria-hidden='true'
                    xmlns='http://www.w3.org/2000/svg'
                    fill='currentColor'
                    viewBox='0 0 20 16'
                  >
                    <path d='m10.036 8.278 9.258-7.79A1.979 1.979 0 0 0 18 0H2A1.987 1.987 0 0 0 .641.541l9.395 7.737Z' />
                    <path d='M11.241 9.817c-.36.275-.801.425-1.255.427-.428 0-.845-.138-1.187-.395L0 2.6V14a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V2.5l-8.759 7.317Z' />
                  </svg>
                </div>
                <input
                  className='focus:ring-primary-500 focus:border-primary-500 dark:focus:ring-primary-500 dark:focus:border-primary-500 block w-full rounded-lg border border-gray-300 bg-white p-3 pl-9 text-sm text-gray-900 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 sm:rounded-none sm:rounded-l-lg'
                  placeholder='Coloque o seu email'
                  type='email'
                  id='email'
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
              <div>
                <button
                  type='submit'
                  className='my-4 inline-flex w-full cursor-pointer rounded-lg border border-gray-300 px-5 py-3 text-center text-sm font-medium transition-all duration-500 ease-in-out hover:bg-gray-300 focus:outline-none focus:ring-4 focus:ring-gray-400 dark:border-dracula-purple-900 dark:bg-dracula-purple-900 dark:text-black dark:hover:border-white dark:hover:bg-gray-900 dark:hover:text-white sm:rounded-none sm:rounded-r-lg'
                  aria-label='Assinar newsletter'
                >
                  Assinar
                </button>
              </div>
            </div>
            <div className='mx-auto max-w-screen-sm text-left text-sm text-gray-500 dark:text-gray-300'>
              Ao assinar, você concorda com os{' '}
              <a
                href='/terms/service'
                className='font-medium text-dracula-purple hover:underline dark:text-dracula-purple'
              >
                Termos de Serviço
              </a>{' '}
              e a{' '}
              <a
                href='/terms/privacy'
                className='font-medium text-dracula-purple hover:underline dark:text-dracula-purple'
              >
                Política de Privacidade
              </a>
              .
            </div>
          </form>
          <p className='pt-8 text-sm text-gray-500 dark:text-gray-300'>
            Assine o{' '}
            <a
              href={`${API_URL}/rss`}
              className='inline-flex items-center font-medium text-cyan-500 hover:underline dark:text-cyan-600'
            >
              Feed RSS
            </a>
          </p>
        </div>
      </div>
    </section>
  )
}
