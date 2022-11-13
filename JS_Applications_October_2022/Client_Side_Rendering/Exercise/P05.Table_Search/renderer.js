export function renderer(root, render, htlm, data) {
    const tableRow = htlm`
    
        ${Object.values(data).map(item => htlm`
            <tr>
                <td>${item.firstName} ${item.lastName}</td>
                <td>${item.email}</td>
                <td>${item.course}</td>
            </tr>
        `)}
    `

    render(tableRow, root);
}