// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';

// https://astro.build/config
export default defineConfig({
	site: 'https://andresmz-ec.github.io/net-telerik-prueba/',
	base: '/net-telerik-prueba',
	outDir: './docs',
	integrations: [
		starlight({
			title: 'Bilbo S.A',
			social: [{ icon: 'github', label: 'GitHub', href: 'https://github.com/withastro/starlight' }],
			defaultLocale: 'es',
			sidebar: [
				{
					label: 'Empezando',
					items: [
						{ label: 'Instalación',  slug: 'instalacion' },
						{ label: 'Configuración',  slug: 'configuracion' },
					],
				},
				{
					label: 'Componentes',
					items: [
						{ label: "Iconos", slug: "componentes/iconos" },
						{ label: "Notificación", slug: "componentes/notificacion" },
					]
				},
				{
					label: 'Referencia Técnica',
					autogenerate: { directory: 'referencia' },
				},
			],
		}),
	],
});
